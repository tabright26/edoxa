import React from "react";
import ReactDOM from "react-dom";
import PageError404 from "./404";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<PageError404 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
