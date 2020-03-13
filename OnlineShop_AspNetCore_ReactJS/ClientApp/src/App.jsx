import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Home from "./components/home";
import { NavMenu } from "./components/NavMenu";
import PieDetail from "./components/pieDetail";
import PieList from "./components/pieList";
import ShoppingCart from "./components/shoppingCart";
import "font-awesome/css/font-awesome.css";
import About from "./components/about";
import "./custom.css";

class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <React.Fragment>
        <NavMenu />
        <Switch>
          <Route path="/shoppingcart" component={ShoppingCart} />
          <Route path="/pies/:id" component={PieDetail} />
          <Route path="/pies" component={PieList} />
          <Route path="/about" component={About} />
          <Route exact path="/" component={Home} />
        </Switch>
      </React.Fragment>
    );
  }
}

export default App;
