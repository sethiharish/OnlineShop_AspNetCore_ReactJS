import React from "react";
import { Link } from "react-router-dom";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const PieListContent = props => {
  const displayName = "Pie List Content";
  const { pies, error, onLoad } = props;

  return (
    <React.Fragment>
      {error && <ErrorMessage componentName={displayName} />}
      {!error && pies && (
        <div className="col-sm-9">
          <div className="row border">
            {pies.map(pie => (
              <div key={pie.id} className="col-sm-4">
                {!error && !pie.loaded && (
                  <Spinner componentName={displayName} spinnerOnly />
                )}
                <div
                  className="px-4 my-2"
                  style={!pie.loaded ? { display: "none" } : null}
                >
                  <img
                    className="img-fluid"
                    src={pie.thumbnailImageUrl}
                    title={pie.name}
                    alt={pie.name}
                    onLoad={() => onLoad(pie)}
                  ></img>
                  <Link to={`/pies/${pie.id}`}>{pie.name}</Link>
                  <span className="float-right">${pie.price}</span>
                  <p>{pie.shortDescription}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
    </React.Fragment>
  );
};

export default PieListContent;
