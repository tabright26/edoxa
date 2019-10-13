import React from "react";
import { Button } from "reactstrap";
import { connectUserAccountDeposit } from "store/root/user/account/deposit/container";

const DepositButton = ({ actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => actions.showDepositModal(actions, amounts)}>
    Deposit Money
  </Button>
);

export default connectUserAccountDeposit("money")(DepositButton);
