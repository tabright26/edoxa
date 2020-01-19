import React from "react";
import ReactDOM from "react-dom";
import Error401 from ".";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<Error401 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
