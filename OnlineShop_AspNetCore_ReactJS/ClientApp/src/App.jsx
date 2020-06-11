import React, { Component } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import Home from "./components/home/page";
import { NavMenu } from "./components/NavMenu";
import PieDetail from "./components/pieDetail/page";
import PieList from "./components/pieList/page";
import ShoppingCart from "./components/shoppingCart/page";
import About from "./components/about/page";
import NotFound from "./components/notFound/page";
import "font-awesome/css/font-awesome.css";
import "./custom.css";

class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <React.Fragment>
        <NavMenu />
        <Switch>
          <Route path="/react/shoppingcart" component={ShoppingCart} />
          <Route path="/react/pies/:id" component={PieDetail} />
          <Route path="/react/pies" component={PieList} />
          <Route path="/react/about" component={About} />
          <Route path="/react/notfound" component={NotFound} />
          <Route exact path="/react" component={Home} />
          <Redirect exact from="/" to="/react" />
          <Redirect to="/react/notfound" />
        </Switch>
      </React.Fragment>
    );
  }
}

export default App;
