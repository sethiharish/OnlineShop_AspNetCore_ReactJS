import React from "react";
import ErrorMessage from "../common/errorMessage";
import Spinner from "../common/spinner";

const Banner = (props) => {
  const displayName = "Banner";
  const { banner, error, bannerDataLoading, onLoad } = props;

  return (
    <div className="container">
      <div className="row">
        {error && <ErrorMessage componentName={displayName} />}
        {!error && bannerDataLoading && <Spinner componentName={displayName} />}
        {!error && banner && (
          <div
            className="row border mb-2 py-2"
            style={bannerDataLoading ? { display: "none" } : null}
          >
            <div className="col-sm-12">
              <img
                className="img-fluid"
                src={banner.imageUrl}
                alt={banner.name}
                title={banner.description}
                onLoad={() => onLoad(banner)}
              ></img>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Banner;
