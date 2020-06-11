import React, { Component } from "react";
import AboutContent from "./content";
import AboutSideBar from "./sideBar";
import iterationService from "../../services/iterationService";
import workItemService from "../../services/workItemService";

class About extends Component {
  state = {
    workItemData: { workItems: [], error: null },
    iterationData: { iterations: [], selectedIteration: null, error: null },
    iterationDataLoading: true,
    workItemDataLoading: true,
  };

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
      workItemData: { workItems: workItemData.data, error: workItemData.error },
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

  render() {
    const { workItems, error: workItemsError } = this.state.workItemData;
    const {
      iterations,
      selectedIteration,
      error: iterationsError,
    } = this.state.iterationData;
    const { iterationDataLoading, workItemDataLoading } = this.state;

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
          />
        </div>
      </div>
    );
  }
}

export default About;
