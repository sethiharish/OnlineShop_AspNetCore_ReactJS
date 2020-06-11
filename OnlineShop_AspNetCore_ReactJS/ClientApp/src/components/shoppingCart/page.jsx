import React, { Component } from "react";
import ShoppingCartContent from "./content";
import shoppingCartService from "../../services/shoppingCartService";

class ShoppingCart extends Component {
  updateQuantity = 1;

  state = {
    shoppingCartData: { shoppingCartItems: [], error: null },
    shoppingCartDataLoading: true,
  };

  async componentDidMount() {
    const [shoppingCartData] = await Promise.all([
      shoppingCartService.getShoppingCartItems(),
    ]);

    this.setState({
      shoppingCartData: {
        shoppingCartItems: shoppingCartData.data,
        error: shoppingCartData.error,
      },
      shoppingCartDataLoading: false,
    });
  }

  handleQuantityDecreased = async (shoppingCartItem) => {
    const result = await shoppingCartService.decreaseItemQuantity(
      shoppingCartItem.pieId,
      this.updateQuantity
    );
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [
        ...shoppingCartDataClone.shoppingCartItems,
      ];
      const index = shoppingCartItemsClone.indexOf(shoppingCartItem);
      const shoppingCartItemClone = { ...shoppingCartItemsClone[index] };
      shoppingCartItemClone.quantity -= this.updateQuantity;
      if (shoppingCartItemClone.quantity > 0) {
        shoppingCartItemsClone[index] = shoppingCartItemClone;
        shoppingCartDataClone.shoppingCartItems = shoppingCartItemsClone;
      } else {
        shoppingCartDataClone.shoppingCartItems = shoppingCartItemsClone.filter(
          (i) => i.pieId !== shoppingCartItem.pieId
        );
      }
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  handleQuantityIncreased = async (shoppingCartItem) => {
    const result = await shoppingCartService.increaseItemQuantity(
      shoppingCartItem.pieId,
      this.updateQuantity
    );
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [
        ...shoppingCartDataClone.shoppingCartItems,
      ];
      const index = shoppingCartItemsClone.indexOf(shoppingCartItem);
      const shoppingCartItemClone = { ...shoppingCartItemsClone[index] };
      shoppingCartItemClone.quantity += this.updateQuantity;
      shoppingCartItemsClone[index] = shoppingCartItemClone;
      shoppingCartDataClone.shoppingCartItems = shoppingCartItemsClone;
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  handleItemRemoved = async (shoppingCartItem) => {
    const result = await shoppingCartService.removeItem(shoppingCartItem.pieId);
    if (result.data) {
      const shoppingCartDataClone = { ...this.state.shoppingCartData };
      const shoppingCartItemsClone = [
        ...shoppingCartDataClone.shoppingCartItems,
      ];
      shoppingCartDataClone.shoppingCartItems = shoppingCartItemsClone.filter(
        (i) => i.pieId !== shoppingCartItem.pieId
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
      shoppingCartDataClone.shoppingCartItems = [];
      this.setState({
        shoppingCartData: shoppingCartDataClone,
      });
    }
  };

  render() {
    const { shoppingCartItems, error } = this.state.shoppingCartData;
    const { shoppingCartDataLoading } = this.state;

    return (
      <ShoppingCartContent
        shoppingCartItems={shoppingCartItems}
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
