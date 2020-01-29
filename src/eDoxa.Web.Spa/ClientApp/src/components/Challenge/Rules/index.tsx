import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Register from "components/Challenge/Register";

const Rules: FunctionComponent = () => (
  <Card className="my-4">
    <CardHeader className="bg-gray-900 text-center">
      <strong className="text-uppercase">Rules</strong>
    </CardHeader>
    <CardBody>
      <ol className="pl-3">
        <li>You can register to multiple Challenges at a time.</li>
        <li>
          We start recording your in-game stats when the Challenge state is
          "Started".
        </li>
        <li>
          We only record stats from Solo / Duo Ranked games in Leagues of
          Legends.
        </li>
        <li>You can play multiple matches in the designated time frame.</li>
        <li>
          Based on the "Best of" 1/3/5, we will only use the best scored matches
          to tabulate your final score.
        </li>
        <li>
          Match synchronisation can take up to 120 minutes or more to be
          displayed.
        </li>
        <li>
          The payout will be distributed to winners after the Challenge is in
          the "Closed" state.
        </li>
      </ol>
      <p className="text-uppercase ">
        <strong className="text-primary">
          SMURF ACCOUNTS ARE NOT ALLOWED ON EDOXA.GG, IF CAUGHT, IT'S AN INSTANT
          BAN WITH NO REFUNDS.
        </strong>
      </p>
      <Register className="w-100 text-uppercase" />
    </CardBody>
  </Card>
);

export default Rules;
