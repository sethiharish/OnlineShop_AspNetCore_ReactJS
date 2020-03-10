import http from "./httpService";
import config from "../config.json";

const apiEndPoint = `${config.baseApi}/shoppingcart`;

async function getShoppingCartItems() {
  let results = { data: null, error: null };

  try {
    const { data } = await http.get(apiEndPoint);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function decreaseItemQuantity(pieId, quantity) {
  let results = { data: null, error: null };

  try {
    const { data } = await http.post(apiEndPoint, {
      pieId: pieId,
      action: "DECREASE_ITEM_QUANTITY",
      quantity: quantity
    });
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function increaseItemQuantity(pieId, quantity) {
  let results = { data: null, error: null };

  try {
    const { data } = await http.post(apiEndPoint, {
      pieId: pieId,
      action: "INCREASE_ITEM_QUANTITY",
      quantity: quantity
    });
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function removeItem(pieId) {
  let results = { data: null, error: null };

  try {
    const { data } = await http.post(apiEndPoint, {
      pieId: pieId,
      action: "REMOVE_ITEM"
    });
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function clearCart() {
  let results = { data: null, error: null };

  try {
    const { data } = await http.post(apiEndPoint, { action: "CLEAR_CART" });
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

export default {
  getShoppingCartItems,
  decreaseItemQuantity,
  increaseItemQuantity,
  removeItem,
  clearCart
};
