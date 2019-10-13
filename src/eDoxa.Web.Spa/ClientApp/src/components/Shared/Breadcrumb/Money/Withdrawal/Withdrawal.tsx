import React from "react";
import { Button } from "reactstrap";
import { connectUserAccountWithdrawal } from "store/root/user/account/withdrawal/container";

const Withdrawal = ({ actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => actions.showWithdrawalModal(actions, amounts)}>
    Withdrawal Money
  </Button>
);

export default connectUserAccountWithdrawal("money")(Withdrawal);
