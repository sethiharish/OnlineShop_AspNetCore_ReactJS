import React from "react";

const Spinner = props => {
  const {
    message,
    componentName,
    colSpan,
    spinnerOnly,
    showComponentName
  } = props;

  return (
    <div className={`btn btn-danger m-2 col-sm-${colSpan}`}>
      {!spinnerOnly && (
        <span>
          {message && message}
          {!message &&
            showComponentName &&
            componentName &&
            `Loading ${componentName} data ... `}
          {!message && !showComponentName && "Loading data ... "}
        </span>
      )}
      <span className="spinner-border spinner-border-sm"></span>
    </div>
  );
};

Spinner.defaultProps = {
  colSpan: 4,
  spinnerOnly: false,
  showComponentName: false
};

export default Spinner;
