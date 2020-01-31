import React from "react";
import ReactDOM from "react-dom";
import Error404 from ".";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<Error404 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
