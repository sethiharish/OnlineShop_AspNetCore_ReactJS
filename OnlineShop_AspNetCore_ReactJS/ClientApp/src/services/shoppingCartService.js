import http from "./httpService";

const apiEndPoint = "shoppingcart";

async function getShoppingCartItems() {
  return await http.get(apiEndPoint);
}

async function decreaseItemQuantity(pieId, quantity) {
  return await http.post(apiEndPoint, {
    pieId: pieId,
    action: "DECREASE_ITEM_QUANTITY",
    quantity: quantity
  });
}

async function increaseItemQuantity(pieId, quantity) {
  return await http.post(apiEndPoint, {
    pieId: pieId,
    action: "INCREASE_ITEM_QUANTITY",
    quantity: quantity
  });
}

async function removeItem(pieId) {
  return await http.post(apiEndPoint, {
    pieId: pieId,
    action: "REMOVE_ITEM"
  });
}

async function clearCart() {
  return await http.post(apiEndPoint, {
    action: "CLEAR_CART"
  });
}

export default {
  getShoppingCartItems,
  decreaseItemQuantity,
  increaseItemQuantity,
  removeItem,
  clearCart
};
