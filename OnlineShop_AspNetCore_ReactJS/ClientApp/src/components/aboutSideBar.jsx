import React from "react";
import ListGroup from "./common/listGroup";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const AboutSideBar = props => {
  const displayName = "About Side Bar";
  const {
    iterations,
    selectedIteration,
    iterationDataLoading,
    error,
    onIterationSelected
  } = props;

  return (
    <div className="col-sm-3">
      {error && <ErrorMessage componentName={displayName} colSpan={12} />}
      {!error && iterationDataLoading && (
        <Spinner componentName={displayName} spinnerOnly />
      )}
      {!error && iterations && (
        <ListGroup
          items={iterations}
          selectedItem={selectedIteration}
          onItemSelected={onIterationSelected}
        />
      )}
    </div>
  );
};

export default AboutSideBar;
