import React from "react";
import ReactDOM from "react-dom";
import Error403 from ".";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<Error403 />, div);
  ReactDOM.unmountComponentAtNode(div);
});
