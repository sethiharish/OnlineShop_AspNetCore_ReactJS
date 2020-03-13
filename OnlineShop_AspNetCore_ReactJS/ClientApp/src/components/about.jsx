import React, { Component } from "react";
import AboutContent from "./aboutContent";
import AboutSideBar from "./aboutSideBar";
import iterationService from "../services/iterationService";
import workItemService from "../services/workItemService";

class About extends Component {
  state = {
    itemData: { items: [], error: null },
    iterationData: { iterations: [], selectedIteration: null, error: null },
    iterationDataLoading: true,
    itemDataLoading: true
  };

  async componentDidMount() {
    const iterationData = await iterationService.getIterations();
    this.setState({
      iterationData: {
        iterations: iterationData.data ? iterationData.data : [],
        selectedIteration: iterationData.data ? iterationData.data[0] : null,
        error: iterationData.error
      },
      iterationDataLoading: false
    });

    const itemData = await workItemService.getWorkItems();
    this.setState({
      itemData: { items: itemData.data, error: itemData.error },
      itemDataLoading: false
    });
  }

  handleIterationSelected = iteration => {
    const iterationDataClone = { ...this.state.iterationData };
    iterationDataClone.selectedIteration = iteration;
    this.setState({ iterationData: iterationDataClone });
  };

  handleItemImageLoad = item => {
    const itemDataClone = { ...this.state.itemData };
    const itemsClone = [...itemDataClone.items];
    const index = itemsClone.indexOf(item);
    const itemClone = { ...itemsClone[index] };
    itemClone.loaded = true;
    itemsClone[index] = itemClone;
    itemDataClone.items = itemsClone;
    this.setState({ itemData: itemDataClone });
  };

  render() {
    const { items, error: itemsError } = this.state.itemData;
    const {
      iterations,
      selectedIteration,
      error: iterationsError
    } = this.state.iterationData;
    const { iterationDataLoading, itemDataLoading } = this.state;

    const filteredItems = items
      ? items.filter(
          item =>
            (selectedIteration && item.iterationId === selectedIteration.id) ||
            (!selectedIteration &&
              item.iterationName === "Application Overview")
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
            items={filteredItems}
            itemDataLoading={itemDataLoading}
            error={itemsError}
            onLoad={this.handleItemImageLoad}
          />
        </div>
      </div>
    );
  }
}

export default About;
