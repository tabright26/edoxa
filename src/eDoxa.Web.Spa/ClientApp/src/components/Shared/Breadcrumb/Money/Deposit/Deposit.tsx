import React from "react";
import { Button } from "reactstrap";
import { withUserAccountDeposit } from "store/root/user/account/deposit/container";
import { withModals } from "store/middlewares/modal/container";
import { compose } from "recompose";

const DepositButton = ({ modals, actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => modals.showDepositModal(actions, amounts)}>
    Deposit Money
  </Button>
);

const enhance = compose<any, any>(
  withUserAccountDeposit("money"),
  withModals
);

export default enhance(DepositButton);
