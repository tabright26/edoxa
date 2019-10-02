import React from "react";
import Update from "./Update";
import renderer from "react-test-renderer";
import moment from "moment";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const personalInfo = {
    firstName: "Boby",
    lastName: "Bob",
    birthDate: moment(new Date(2000, 2, 1)).unix(),
    gender: "Male"
  };
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Update initialValues={personalInfo} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
