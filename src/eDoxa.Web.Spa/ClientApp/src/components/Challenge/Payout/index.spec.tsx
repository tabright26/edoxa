import React from "react";
import renderer from "react-test-renderer";
import Payout from ".";
import { ChallengePayout } from "types";

it("renders without crashing", () => {
  // Arrange
  const payout: ChallengePayout = {
    prizePool: {
      currency: "money",
      amount: 0
    },
    buckets: []
  };

  //Act
  const tree = renderer
    .create(<Payout challengeId="123" payout={payout} />)
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
