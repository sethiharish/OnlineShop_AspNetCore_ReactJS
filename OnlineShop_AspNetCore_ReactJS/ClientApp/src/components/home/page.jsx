import React, { Component } from "react";
import Banner from "./bannerContent";
import PiesOfTheWeek from "./piesOfTheWeekContent";
import Modal from "../common/modal";
import bannerService from "../../services/bannerService";
import pieService from "../../services/pieService";
import generateRandomNumber from "../../utils/generateRandomNumber";
import shoppingCartService from "../../services/shoppingCartService";

class Home extends Component {
  state = {
    bannerData: { banners: [], activeBanner: {}, error: null },
    pieData: { pies: [], selectedPie: null, error: null },
    bannerDataLoading: true,
    pieDataLoading: true,
    showModal: false,
  };

  modalContent = (selectedPie) =>
    selectedPie ? (
      <img
        className="img-fluid"
        src={selectedPie.imageUrl}
        title={selectedPie.name}
        alt={selectedPie.name}
      ></img>
    ) : null;

  async componentDidMount() {
    const [bannerData, pieData] = await Promise.all([
      bannerService.getBanners(),
      pieService.getPiesOfTheWeek(),
    ]);

    this.setState({
      pieData: { pies: pieData.data, selectedPie: null, error: pieData.error },
      bannerData: {
        banners: bannerData.data,
        activeBanner: {},
        error: bannerData.error,
      },
    });

    // Suppressing Random Banner display
    // const activeBanner = await this.getBannerRandomly(bannerData.data);
    const activeBanner =
      bannerData.data && bannerData.data.length !== 0 ? bannerData.data[0] : {};
    const bannerDataClone = { ...this.state.bannerData };
    bannerDataClone.activeBanner = activeBanner;
    this.setState({ bannerData: bannerDataClone });
  }

  getBannerRandomly = (banners) => {
    return new Promise((resolve) => {
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

  handleBannerImageLoad = (banner) => {
    this.setState({ bannerDataLoading: false });
  };

  handlePieImageLoad = (pie) => {
    const piesClone = [...this.state.pieData.pies];
    const index = piesClone.indexOf(pie);
    const pieClone = { ...piesClone[index] };
    pieClone.isLoading = false;
    const pieDataLoading =
      piesClone.filter((p) => p.isLoading).length === 0 ? false : true;
    this.setState({ pieDataLoading });
  };

  handleAddToCart = async (pie) => {
    const { history } = this.props;
    const result = await shoppingCartService.increaseItemQuantity(pie.id, 1);
    if (result.data) {
      history.push("/react/shoppingcart");
    }
  };

  handleShowHideModal = (pie) => {
    const pieDataClone = { ...this.state.pieData };
    const pieClone = { ...pie };
    pieDataClone.selectedPie = pieClone;
    this.setState({
      pieData: pieDataClone,
      showModal: !this.state.showModal,
    });
  };

  render() {
    const { pieDataLoading, bannerDataLoading, showModal } = this.state;
    const { activeBanner, error: bannerDataError } = this.state.bannerData;
    const { pies, selectedPie, error: piesError } = this.state.pieData;

    return (
      <React.Fragment>
        <Modal
          showModal={showModal}
          onHideModal={this.handleShowHideModal}
          data={selectedPie}
        >
          {this.modalContent(selectedPie)}
        </Modal>
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
          onAddToCart={this.handleAddToCart}
          onShowModal={this.handleShowHideModal}
        />
      </React.Fragment>
    );
  }
}

export default Home;
