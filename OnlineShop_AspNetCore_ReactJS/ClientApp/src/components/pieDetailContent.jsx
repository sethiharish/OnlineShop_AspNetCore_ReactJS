import React from "react";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const PieDetailContent = props => {
  const displayName = "Pie Detail";
  const { pie, error, onLoad, pieDataLoading } = props;
  const displayStyle = pieDataLoading ? { display: "none" } : null;

  return (
    <div className="container">
      <div className="row mb-2">
        {error && <ErrorMessage componentName={displayName} />}
        {!error && pieDataLoading && <Spinner componentName={displayName} />}
        {!error && pie && (
          <div className="row border pb-2" style={displayStyle}>
            <div className="col-sm-9">
              <span>
                <h5>
                  {pie.name} - <small>{pie.shortDescription}</small>
                </h5>
              </span>
              <img
                className="img-fluid"
                src={pie.imageUrl}
                title={pie.name}
                alt={pie.name}
                onLoad={onLoad}
              ></img>
            </div>
            <div className="col-sm-3 mt-5">
              <p>
                <small>{pie.longDescription}</small>
              </p>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default PieDetailContent;
