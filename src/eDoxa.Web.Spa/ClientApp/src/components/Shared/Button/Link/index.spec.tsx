import React from "react";
import renderer from "react-test-renderer";
import { Link } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Link>Link</Link>).toJSON();
  expect(tree).toMatchSnapshot();
});
