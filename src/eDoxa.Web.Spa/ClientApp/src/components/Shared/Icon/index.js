import React, { Component } from "react";

class Icon extends Component {
  static Money = React.lazy(() => import("./Money"));
  static Token = React.lazy(() => import("./Token"));
}

export default Icon;
