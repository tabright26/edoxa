import React from "react";
import { Button } from "reactstrap";
import { connectUserAccountDeposit } from "store/user/account/deposit/container";

const DepositButton = ({ actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => actions.showDepositModal(actions, amounts)}>
    Buy Token
  </Button>
);

export default connectUserAccountDeposit("token")(DepositButton);
