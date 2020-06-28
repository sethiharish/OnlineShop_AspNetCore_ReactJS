import React, { Component } from "react";
import pieService from "../../services/pieService";
import PieDetailContent from "./content";
import shoppingCartService from "../../services/shoppingCartService";

class PieDetail extends Component {
  state = { pieData: {}, pieDataLoading: true };

  async componentDidMount() {
    const { match } = this.props;

    const [pieData] = await Promise.all([pieService.getPie(match.params.id)]);

    this.setState({ pieData: { pie: pieData.data, error: pieData.error } });
  }

  handleImageLoad = () => {
    this.setState({ pieDataLoading: false });
  };

  handleAddToCart = async (pie) => {
    const { history } = this.props;
    const result = await shoppingCartService.increaseItemQuantity(pie.id, 1);
    if (result.data) {
      history.push("/react/shoppingcart");
    }
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
        onAddToCart={this.handleAddToCart}
      />
    );
  }
}

export default PieDetail;
