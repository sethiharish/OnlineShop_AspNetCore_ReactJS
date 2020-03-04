import React from "react";
import ListGroup from "./common/listGroup";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const PieListSideBar = props => {
  const displayName = "Pie List Side Bar";
  const {
    categories,
    selectedCategory,
    categoryDataLoading,
    error,
    onCategorySelected
  } = props;

  return (
    <div className="col-sm-3">
      {error && <ErrorMessage componentName={displayName} colSpan={12} />}
      {!error && categoryDataLoading && (
        <Spinner componentName={displayName} spinnerOnly />
      )}
      {!error && categories && (
        <ListGroup
          items={categories}
          selectedItem={selectedCategory}
          onItemSelected={onCategorySelected}
        />
      )}
    </div>
  );
};

export default PieListSideBar;
