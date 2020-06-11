import React from "react";

const ErrorMessage = (props) => {
  const { message, componentName, colSpan, showComponentName } = props;

  return (
    <div className={`alert alert-danger col-sm-${colSpan}`}>
      {message && message}
      {!message &&
        showComponentName &&
        componentName &&
        `Some error occured, while getting the ${componentName} data!`}
      {!message &&
        !showComponentName &&
        "Some error occured, while getting the data!"}
    </div>
  );
};

ErrorMessage.defaultProps = {
  colSpan: 6,
  showComponentName: false,
};

export default ErrorMessage;
