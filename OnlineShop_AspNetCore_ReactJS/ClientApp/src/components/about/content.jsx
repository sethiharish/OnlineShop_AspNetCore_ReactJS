import React from "react";
import ErrorMessage from "../common/errorMessage";
import Spinner from "../common/spinner";

const AboutContent = (props) => {
  const displayName = "About Content";
  const { workItems, error, workItemDataLoading, onLoad, onShowModal } = props;

  return (
    <div className="col-sm-9">
      <div>
        {error && <ErrorMessage componentName={displayName} />}
        {!error && workItemDataLoading && (
          <Spinner componentName={displayName} spinnerOnly />
        )}
        {!error && workItems && (
          <div className="row mb-2">
            {workItems.map((workItem) => (
              <React.Fragment key={workItem.id}>
                {workItem.iterationName === "Application Overview" && (
                  <div style={workItemDataLoading ? { display: "none" } : null}>
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
                {workItem.iterationName !== "Application Overview" && (
                  <div className="col-sm-6">
                    {!error && !workItem.loaded && (
                      <Spinner componentName={displayName} spinnerOnly />
                    )}
                    <div
                      className="border px-4 mb-2"
                      style={!workItem.loaded ? { display: "none" } : null}
                    >
                      <h5>{workItem.name}</h5>
                      <img
                        className="img-fluid"
                        style={{ cursor: "zoom-in" }}
                        src={workItem.imageUrl}
                        title={workItem.name}
                        alt={workItem.name}
                        onLoad={() => onLoad(workItem)}
                        onClick={() => onShowModal(workItem)}
                      ></img>
                    </div>
                  </div>
                )}
              </React.Fragment>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default AboutContent;
