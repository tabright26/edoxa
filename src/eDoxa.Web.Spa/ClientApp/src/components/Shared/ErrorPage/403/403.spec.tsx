import React from "react";
import ReactDOM from "react-dom";
import PageError403 from "./403";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<PageError403 />, div);
  ReactDOM.unmountComponentAtNode(div);
});