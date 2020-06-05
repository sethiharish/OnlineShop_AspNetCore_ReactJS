import React from "react";
import ErrorMessage from "./common/errorMessage";
import Spinner from "./common/spinner";

const AboutContent = (props) => {
  const displayName = "About Content";
  const { items, error, itemDataLoading, onLoad } = props;

  return (
    <div className="col-sm-9">
      <div>
        {error && <ErrorMessage componentName={displayName} />}
        {!error && itemDataLoading && (
          <Spinner componentName={displayName} spinnerOnly />
        )}
        {!error && items && (
          <React.Fragment>
            {items.map((item) => (
              <React.Fragment key={item.id}>
                {item.iterationName === "Application Overview" && (
                  <div style={itemDataLoading ? { display: "none" } : null}>
                    <p>
                      <b>Author:</b> Harish Sethi
                    </p>
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
                        <li>Front end - ReactJS &amp; Bootstrap</li>
                        <li>
                          Back end - REST / Web APIs using Asp.Net Core, C#
                          &amp; SQLite
                        </li>
                        <li>
                          Azure board for work item creation &amp; tracking
                        </li>
                        <li>
                          Azure Repository (GIT) for maintaining the source code
                          history
                        </li>
                        <li>
                          Azure pipelines for running continuous integration
                          build
                        </li>
                        <li>
                          Google cloud build for running continuous deployment
                          build, to Google cloud app engine (PAAS)
                        </li>
                        <li>Docker / Containers</li>
                        <li>
                          Test driven development / Unit testing / Integration
                          testing
                        </li>
                      </ul>
                    </div>
                  </div>
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
          </React.Fragment>
        )}
      </div>
    </div>
  );
};

export default AboutContent;
