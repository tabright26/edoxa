import React from "react";
import KickMember from "./KickMember";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <KickMember initialValues={{ clanId: "", memberId: "" }} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
