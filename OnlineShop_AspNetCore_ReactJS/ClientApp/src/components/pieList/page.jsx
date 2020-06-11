import React, { Component } from "react";
import PieListContent from "./content";
import PieListSideBar from "./sideBar";
import pieService from "../../services/pieService";
import categoryService from "../../services/categoryService";
import shoppingCartService from "../../services/shoppingCartService";

class PieList extends Component {
  state = {
    pieData: { pies: [], error: null },
    categoryData: { categories: [], selectedCategory: null, error: null },
    categoryDataLoading: true,
    pieDataLoading: true,
  };

  async componentDidMount() {
    const [categoryData, pieData] = await Promise.all([
      categoryService.getCategories(),
      pieService.getPies(),
    ]);

    const categoryAll = { id: 0, name: "All Pies" };
    this.setState({
      pieData: { pies: pieData.data, error: pieData.error },
      pieDataLoading: false,
      categoryData: {
        categories: categoryData.data
          ? [categoryAll, ...categoryData.data]
          : [],
        selectedCategory: categoryAll,
        error: categoryData.error,
      },
      categoryDataLoading: false,
    });
  }

  handleCategorySelected = (category) => {
    const categoryDataClone = { ...this.state.categoryData };
    categoryDataClone.selectedCategory = category;
    this.setState({ categoryData: categoryDataClone });
  };

  handlePieImageLoad = (pie) => {
    const pieDataClone = { ...this.state.pieData };
    const piesClone = [...pieDataClone.pies];
    const index = piesClone.indexOf(pie);
    const pieClone = { ...piesClone[index] };
    pieClone.loaded = true;
    piesClone[index] = pieClone;
    pieDataClone.pies = piesClone;
    this.setState({ pieData: pieDataClone });
  };

  handleAddToCart = async (pie) => {
    const { history } = this.props;
    const result = await shoppingCartService.increaseItemQuantity(pie.id, 1);
    if (result.data) {
      history.replace("/react/shoppingcart");
    }
  };

  render() {
    const { pies, error: piesError } = this.state.pieData;
    const {
      categories,
      selectedCategory,
      error: categoriesError,
    } = this.state.categoryData;
    const { categoryDataLoading, pieDataLoading } = this.state;

    const filteredPies = pies.filter(
      (pie) =>
        !selectedCategory ||
        selectedCategory.id === 0 ||
        pie.categoryId === selectedCategory.id
    );

    return (
      <div className="container">
        <div className="row">
          <PieListSideBar
            categories={categories}
            selectedCategory={selectedCategory}
            categoryDataLoading={categoryDataLoading}
            error={categoriesError}
            onCategorySelected={this.handleCategorySelected}
          />
          <PieListContent
            pies={filteredPies}
            pieDataLoading={pieDataLoading}
            error={piesError}
            onLoad={this.handlePieImageLoad}
            onAddToCart={this.handleAddToCart}
          />
        </div>
      </div>
    );
  }
}

export default PieList;
