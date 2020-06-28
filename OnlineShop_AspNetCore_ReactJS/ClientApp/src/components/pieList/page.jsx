import React, { Component } from "react";
import PieListContent from "./content";
import PieListSideBar from "./sideBar";
import Modal from "../common/modal";
import pieService from "../../services/pieService";
import categoryService from "../../services/categoryService";
import shoppingCartService from "../../services/shoppingCartService";

class PieList extends Component {
  state = {
    pieData: { pies: [], selectedPie: null, error: null },
    categoryData: { categories: [], selectedCategory: null, error: null },
    categoryDataLoading: true,
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
    const [categoryData, pieData] = await Promise.all([
      categoryService.getCategories(),
      pieService.getPies(),
    ]);

    const categoryAll = { id: 0, name: "All Pies" };
    this.setState({
      pieData: { pies: pieData.data, selectedPie: null, error: pieData.error },
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
    const { pies, selectedPie, error: piesError } = this.state.pieData;

    const {
      categories,
      selectedCategory,
      error: categoriesError,
    } = this.state.categoryData;

    const { categoryDataLoading, pieDataLoading, showModal } = this.state;

    const filteredPies = pies.filter(
      (pie) =>
        !selectedCategory ||
        selectedCategory.id === 0 ||
        pie.categoryId === selectedCategory.id
    );

    return (
      <div className="container">
        <div className="row">
          <Modal
            showModal={showModal}
            onHideModal={this.handleShowHideModal}
            data={selectedPie}
          >
            {this.modalContent(selectedPie)}
          </Modal>
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
            onShowModal={this.handleShowHideModal}
          />
        </div>
      </div>
    );
  }
}

export default PieList;
