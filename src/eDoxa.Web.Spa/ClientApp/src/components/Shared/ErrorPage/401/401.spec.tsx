import React from "react";
import ReactDOM from "react-dom";
import PageError401 from "./401";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<PageError401 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
