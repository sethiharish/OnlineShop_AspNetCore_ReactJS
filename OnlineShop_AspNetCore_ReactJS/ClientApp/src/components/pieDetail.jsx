import React, { Component } from "react";
import pieService from "../services/pieService";
import PieDetailContent from "./pieDetailContent";

class PieDetail extends Component {
  state = { pieData: {}, pieDataLoading: true };

  async componentDidMount() {
    const { match } = this.props;
    const { data: pie, error } = await pieService.getPie(match.params.id);
    this.setState({ pieData: { pie, error } });
  }

  handleImageLoad = () => {
    this.setState({ pieDataLoading: false });
  };

  render() {
    const { pieDataLoading } = this.state;
    const { pie, error } = this.state.pieData;
    return (
      <PieDetailContent
        pie={pie}
        error={error}
        onLoad={this.handleImageLoad}
        pieDataLoading={pieDataLoading}
      />
    );
  }
}

export default PieDetail;
