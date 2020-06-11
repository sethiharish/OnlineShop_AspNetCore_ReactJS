import React, { Component } from "react";
import ShoppingCartContent from "./content";
import shoppingCartService from "../../services/shoppingCartService";

class ShoppingCart extends Component {
  updateQuantity = 1;

  state = {
    shoppingCartData: { items: [], error: null },
    shoppingCartDataLoading: true,
  };

  async componentDidMount() {
    const shoppingCartData = await shoppingCartService.getShoppingCartItems();
    this.setState({
      shoppingCartData: {
        items: shoppingCartData.data,
        error: shoppingCartData.error,
      },
      shoppingCartDataLoading: false,
    });
  }

  handleQuantityDecreased = async (item) => {
    const result = await shoppingCartService.decreaseItemQuantity(
      item.pieId,
      this.updateQuantity
    );
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [...shoppingCartDataClone.items];
      const index = shoppingCartItemsClone.indexOf(item);
      const shoppingCartItemClone = { ...shoppingCartItemsClone[index] };
      shoppingCartItemClone.quantity -= this.updateQuantity;
      if (shoppingCartItemClone.quantity > 0) {
        shoppingCartItemsClone[index] = shoppingCartItemClone;
        shoppingCartDataClone.items = shoppingCartItemsClone;
      } else {
        shoppingCartDataClone.items = shoppingCartItemsClone.filter(
          (i) => i.pieId !== item.pieId
        );
      }
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  handleQuantityIncreased = async (item) => {
    const result = await shoppingCartService.increaseItemQuantity(
      item.pieId,
      this.updateQuantity
    );
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [...shoppingCartDataClone.items];
      const index = shoppingCartItemsClone.indexOf(item);
      const shoppingCartItemClone = { ...shoppingCartItemsClone[index] };
      shoppingCartItemClone.quantity += this.updateQuantity;
      shoppingCartItemsClone[index] = shoppingCartItemClone;
      shoppingCartDataClone.items = shoppingCartItemsClone;
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  handleItemRemoved = async (item) => {
    const result = await shoppingCartService.removeItem(item.pieId);
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [...shoppingCartDataClone.items];
      shoppingCartDataClone.items = shoppingCartItemsClone.filter(
        (i) => i.pieId !== item.pieId
      );
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  handleCartCleared = async () => {
    const result = await shoppingCartService.clearCart();
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      shoppingCartDataClone.items = [];
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  render() {
    const { items, error } = this.state.shoppingCartData;
    const { shoppingCartDataLoading } = this.state;

    return (
      <ShoppingCartContent
        items={items}
        error={error}
        shoppingCartDataLoading={shoppingCartDataLoading}
        onQuantityIncreased={this.handleQuantityIncreased}
        onQuantityDecreased={this.handleQuantityDecreased}
        onItemRemoved={this.handleItemRemoved}
        onCartCleared={this.handleCartCleared}
      />
    );
  }
}

export default ShoppingCart;
