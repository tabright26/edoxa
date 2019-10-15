import React from "react";
import renderer from "react-test-renderer";
import Paginate from "./Paginate";

it("renders without crashing", () => {
  const tree = renderer.create(<Paginate totalItems={5} onPageChange={jest.fn()} />).toJSON();
  expect(tree).toMatchSnapshot();
});
