import React from "react";
import Update from "./Update";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const personalInfo = {
    firstName: "Boby",
    lastName: "Bob",
    birthDate: new Date(2000, 2, 1),
    gender: "Male"
  };
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Update personalInfo={personalInfo} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
