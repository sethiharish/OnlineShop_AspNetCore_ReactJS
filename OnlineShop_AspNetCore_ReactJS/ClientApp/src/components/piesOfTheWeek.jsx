﻿import React from "react";
import { Link } from "react-router-dom";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const PiesOfTheWeek = props => {
  const displayName = "Pies Of The Week";
  const { pies, error, pieDataLoading, onLoad } = props;
  const displayStyle = pieDataLoading ? { display: "none" } : null;

  return (
    <div className="container">
      <div className="row">
        {error && <ErrorMessage componentName={displayName} />}
        {!error && pieDataLoading && <Spinner componentName={displayName} />}
        {!error && pies && (
          <React.Fragment>
            <div className="row" style={displayStyle}>
              <h5>
                Our pies of the week, <small>specially picked for you!</small>
              </h5>
            </div>

            <div className="row border mb-3" style={displayStyle}>
              {pies.map(pie => (
                <div key={pie.id} className="col-sm-4">
                  <div className="px-4 my-2">
                    <img
                      className="img-fluid"
                      src={pie.thumbnailImageUrl}
                      title={pie.name}
                      alt={pie.name}
                      onLoad={() => onLoad(pie)}
                    ></img>
                    <Link to="">{pie.name}</Link>
                    <span className="float-right">${pie.price}</span>
                    <p>{pie.shortDescription}</p>
                  </div>
                </div>
              ))}
            </div>
          </React.Fragment>
        )}
      </div>
    </div>
  );
};

export default PiesOfTheWeek;