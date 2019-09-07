import React from "react";
import ReactDOM from "react-dom";
import PageError500 from "./500";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<PageError500 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
