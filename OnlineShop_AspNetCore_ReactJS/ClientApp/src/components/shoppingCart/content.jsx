import React from "react";
import ErrorMessage from "../common/errorMessage";
import Spinner from "../common/spinner";

const ShoppingCartContent = (props) => {
  const displayName = "Shopping Cart Content";
  const {
    shoppingCartItems,
    error,
    shoppingCartDataLoading,
    onQuantityIncreased,
    onQuantityDecreased,
    onItemRemoved,
    onCartCleared,
  } = props;

  return (
    <div className="container">
      <div className="row">
        {error && <ErrorMessage componentName={displayName} />}
        {!error && shoppingCartDataLoading && (
          <Spinner componentName={displayName} />
        )}
        {!error &&
          !shoppingCartDataLoading &&
          shoppingCartItems &&
          shoppingCartItems.length === 0 && (
            <h5>There are no items in the cart</h5>
          )}
        {!error &&
          !shoppingCartDataLoading &&
          shoppingCartItems &&
          shoppingCartItems.length !== 0 && (
            <React.Fragment>
              <div
                className="w-100"
                style={shoppingCartDataLoading ? { display: "none" } : null}
              >
                <h5 style={{ verticalAlign: "center" }}>
                  Your shopping cart
                  <button
                    className="btn btn-danger mr-4 mb-4"
                    title="Clear Cart"
                    onClick={onCartCleared}
                    style={{ float: "right" }}
                  >
                    Clear Cart <i className="fa fa-trash"></i>
                  </button>
                </h5>
              </div>
              <table
                className="table table-bordered table-striped"
                style={shoppingCartDataLoading ? { display: "none" } : null}
              >
                <thead>
                  <tr>
                    <th className="text-left">Pie</th>
                    <th className="text-center">Selected amount</th>
                    <th className="text-right">Price</th>
                    <th className="text-right">Subtotal</th>
                    <th className="text-center"></th>
                  </tr>
                </thead>
                <tbody>
                  {shoppingCartItems.map((shoppingCartItem) => (
                    <tr key={shoppingCartItem.id}>
                      <td className="text-left">
                        <img
                          className="w-25"
                          src={shoppingCartItem.pieThumbnailImageUrl}
                          title={shoppingCartItem.pieName}
                          alt={shoppingCartItem.pieName}
                        />
                        <span
                          className="ml-2"
                          style={{ verticalAlign: "top", fontWeight: "bold" }}
                        >
                          {shoppingCartItem.pieName}
                        </span>
                      </td>
                      <td className="text-center">
                        {shoppingCartItem.quantity}
                      </td>
                      <td className="text-right">
                        {shoppingCartItem.piePrice.toFixed(2)}
                      </td>
                      <td className="text-right">
                        {(
                          shoppingCartItem.quantity * shoppingCartItem.piePrice
                        ).toFixed(2)}
                      </td>
                      <td className="text-center">
                        <button
                          className="btn btn-danger"
                          title="+1"
                          onClick={() => onQuantityIncreased(shoppingCartItem)}
                        >
                          <i className="fa fa-plus"></i>
                        </button>
                        <button
                          className="btn btn-danger m-2"
                          title="-1"
                          onClick={() => onQuantityDecreased(shoppingCartItem)}
                        >
                          <i className="fa fa-minus"></i>
                        </button>
                        <button
                          className="btn btn-danger"
                          title="Remove Item"
                          onClick={() => onItemRemoved(shoppingCartItem)}
                        >
                          <i className="fa fa-times"></i>
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr>
                    <td colSpan="3" className="text-right">
                      Total:
                    </td>
                    <td className="text-right">
                      {shoppingCartItems
                        .reduce(
                          (a, b) => ({
                            quantity: 1,
                            piePrice:
                              a.quantity * a.piePrice + b.quantity * b.piePrice,
                          }),
                          { quantity: 1, piePrice: 0 }
                        )
                        .piePrice.toFixed(2)}
                    </td>
                    <td></td>
                  </tr>
                </tfoot>
              </table>
            </React.Fragment>
          )}
      </div>
    </div>
  );
};

export default ShoppingCartContent;
