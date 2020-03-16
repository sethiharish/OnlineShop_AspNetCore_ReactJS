import React from "react";
import ErrorMessage from "./common/errorMessage";

const NotFound = () => {
  return (
    <div className="container">
      <div className="row">
        <ErrorMessage message="The resource you are looking for, is not found!" />
      </div>
    </div>
  );
};

export default NotFound;
