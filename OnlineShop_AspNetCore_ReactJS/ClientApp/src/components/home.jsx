import React, { Component } from "react";
import Banner from "./banner";
import PiesOfTheWeek from "./piesOfTheWeek";
import bannerService from "../services/bannerService";
import pieService from "../services/pieService";
import generateRandomNumber from "../utils/generateRandomNumber";

class Home extends Component {
  state = {
    bannerData: { banners: [], activeBanner: {}, error: null },
    pieData: { pies: [], error: null },
    bannerDataLoading: true,
    pieDataLoading: true
  };

  async componentDidMount() {
    const pieData = await pieService.getPiesOfTheWeek();
    this.setState({
      pieData: { pies: pieData.data, error: pieData.error }
    });

    const bannerData = await bannerService.getBanners();
    this.setState({
      bannerData: {
        banners: bannerData.data,
        activeBanner: {},
        error: bannerData.error
      }
    });

    const activeBanner = await this.getBannerRandomly(bannerData.data);
    const bannerDataClone = { ...this.state.bannerData };
    bannerDataClone.activeBanner = activeBanner;
    this.setState({ bannerData: bannerDataClone });
  }

  getBannerRandomly = banners => {
    return new Promise(resolve => {
      let result = null;
      if (banners) {
        const { length: count } = banners;
        if (count > 0) {
          const index = generateRandomNumber(1, count);
          result = banners[index - 1];
        }
      }
      resolve(result);
    });
  };

  handleBannerImageLoad = banner => {
    this.setState({ bannerDataLoading: false });
  };

  handlePieImageLoad = pie => {
    const piesClone = [...this.state.pieData.pies];
    const index = piesClone.indexOf(pie);
    const pieClone = { ...piesClone[index] };
    pieClone.isLoading = false;
    const pieDataLoading =
      piesClone.filter(p => p.isLoading).length === 0 ? false : true;
    this.setState({ pieDataLoading });
  };

  render() {
    const { pieDataLoading, bannerDataLoading } = this.state;
    const { activeBanner, error: bannerDataError } = this.state.bannerData;
    const { pies, error: piesError } = this.state.pieData;

    return (
      <React.Fragment>
        <Banner
          banner={activeBanner}
          error={bannerDataError}
          onLoad={this.handleBannerImageLoad}
          bannerDataLoading={bannerDataLoading}
        />
        <PiesOfTheWeek
          pies={pies}
          error={piesError}
          onLoad={this.handlePieImageLoad}
          pieDataLoading={pieDataLoading}
        />
      </React.Fragment>
    );
  }
}

export default Home;
