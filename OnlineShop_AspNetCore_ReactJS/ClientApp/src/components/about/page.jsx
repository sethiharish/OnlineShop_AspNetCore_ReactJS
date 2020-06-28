import React, { Component } from "react";
import AboutContent from "./content";
import AboutSideBar from "./sideBar";
import Modal from "../common/modal";
import iterationService from "../../services/iterationService";
import workItemService from "../../services/workItemService";

class About extends Component {
  state = {
    workItemData: { workItems: [], selectedWorkItem: null, error: null },
    iterationData: { iterations: [], selectedIteration: null, error: null },
    iterationDataLoading: true,
    workItemDataLoading: true,
    showModal: false,
  };

  modalContent = (selectedWorkItem) =>
    selectedWorkItem ? (
      <img
        className="img-fluid"
        src={selectedWorkItem.imageUrl}
        title={selectedWorkItem.name}
        alt={selectedWorkItem.name}
      ></img>
    ) : null;

  async componentDidMount() {
    const [iterationData, workItemData] = await Promise.all([
      iterationService.getIterations(),
      workItemService.getWorkItems(),
    ]);

    this.setState({
      iterationData: {
        iterations: iterationData.data ? iterationData.data : [],
        selectedIteration: iterationData.data ? iterationData.data[0] : null,
        error: iterationData.error,
      },
      iterationDataLoading: false,
      workItemData: {
        workItems: workItemData.data,
        selectedWorkItem: null,
        error: workItemData.error,
      },
      workItemDataLoading: false,
    });
  }

  handleIterationSelected = (iteration) => {
    const iterationDataClone = { ...this.state.iterationData };
    iterationDataClone.selectedIteration = iteration;
    this.setState({ iterationData: iterationDataClone });
  };

  handleItemImageLoad = (workItem) => {
    const workItemDataClone = { ...this.state.workItemData };
    const workItemsClone = [...workItemDataClone.workItems];
    const index = workItemsClone.indexOf(workItem);
    const workItemClone = { ...workItemsClone[index] };
    workItemClone.loaded = true;
    workItemsClone[index] = workItemClone;
    workItemDataClone.workItems = workItemsClone;
    this.setState({ workItemData: workItemDataClone });
  };

  handleShowHideModal = (workItem) => {
    const workItemDataClone = { ...this.state.workItemData };
    const workItemClone = { ...workItem };
    workItemDataClone.selectedWorkItem = workItemClone;
    this.setState({
      workItemData: workItemDataClone,
      showModal: !this.state.showModal,
    });
  };

  render() {
    const {
      workItems,
      selectedWorkItem,
      error: workItemsError,
    } = this.state.workItemData;

    const {
      iterations,
      selectedIteration,
      error: iterationsError,
    } = this.state.iterationData;

    const { showModal, iterationDataLoading, workItemDataLoading } = this.state;

    const filteredItems = workItems
      ? workItems.filter(
          (workItem) =>
            (selectedIteration &&
              workItem.iterationId === selectedIteration.id) ||
            (!selectedIteration &&
              workItem.iterationName === "Application Overview")
        )
      : [];

    return (
      <div className="container">
        <div className="row">
          <Modal
            showModal={showModal}
            onHideModal={this.handleShowHideModal}
            data={selectedWorkItem}
          >
            {this.modalContent(selectedWorkItem)}
          </Modal>
          <AboutSideBar
            iterations={iterations}
            selectedIteration={selectedIteration}
            iterationDataLoading={iterationDataLoading}
            error={iterationsError}
            onIterationSelected={this.handleIterationSelected}
          />
          <AboutContent
            workItems={filteredItems}
            workItemDataLoading={workItemDataLoading}
            error={workItemsError}
            onLoad={this.handleItemImageLoad}
            onShowModal={this.handleShowHideModal}
          />
        </div>
      </div>
    );
  }
}

export default About;
