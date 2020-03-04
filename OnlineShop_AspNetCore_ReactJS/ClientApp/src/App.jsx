import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Home from "./components/home";
import { NavMenu } from "./components/NavMenu";
import PieDetail from "./components/pieDetail";
import PieList from "./components/pieList";
import "./custom.css";

class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <React.Fragment>
        <NavMenu />
        <Switch>
          <Route path="/pies/:id" component={PieDetail} />
          <Route path="/pies" component={PieList} />
          <Route exact path="/" component={Home} />
        </Switch>
      </React.Fragment>
    );
  }
}

export default App;
