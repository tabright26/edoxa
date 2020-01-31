import React from "react";
import ReactDOM from "react-dom";
import Error500 from ".";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<Error500 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
