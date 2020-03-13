import React from "react";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const AboutContent = props => {
  const displayName = "About Content";
  const { items, error, itemDataLoading, onLoad } = props;

  return (
    <React.Fragment>
      {error && <ErrorMessage componentName={displayName} />}
      {!error && itemDataLoading && (
        <Spinner componentName={displayName} spinnerOnly />
      )}
      {!error && items && (
        <div className="col-sm-9">
          <div>
            {items.map(item => (
              <React.Fragment key={item.id}>
                {item.iterationName === "Application Overview" && (
                  <React.Fragment>
                    <div>
                      <b>Author:</b> Harish Sethi
                    </div>
                    <div>
                      <b>Online Pie Shop</b> is a fictitious e-commerce
                      application that allows the pie shop company to sell pies
                      online. It is a work in progress and currently there are
                      following pages:
                      <ul>
                        <li>Home Page</li>
                        <li>Pie Details page</li>
                        <li>Pie List page</li>
                        <li>Shopping Cart page</li>
                        <li>About page</li>
                      </ul>
                    </div>
                    <div>
                      It has been built using following <b>technology stack:</b>
                      <ul>
                        <li>Front end - ReactJS</li>
                        <li>
                          Back end - REST / Web Apis using .Net Core, C# and
                          SQLite
                        </li>
                        <li>
                          Azure board for work item creation &amp; tracking
                        </li>
                        <li>Azure Repository</li>
                        <li>
                          Azure pipelines for running Continuous Integration
                          build
                        </li>
                      </ul>
                    </div>
                  </React.Fragment>
                )}
                {item.iterationName !== "Application Overview" && (
                  <div className="row mb-2">
                    {!error && !item.loaded && (
                      <Spinner componentName={displayName} />
                    )}
                    <div
                      className="border px-4 mb-2"
                      style={!item.loaded ? { display: "none" } : null}
                    >
                      <h5>{item.name}</h5>
                      <img
                        className="img-fluid"
                        src={item.imageUrl}
                        title={item.name}
                        alt={item.name}
                        onLoad={() => onLoad(item)}
                      ></img>
                    </div>
                  </div>
                )}
              </React.Fragment>
            ))}
          </div>
        </div>
      )}
    </React.Fragment>
  );
};

export default AboutContent;
