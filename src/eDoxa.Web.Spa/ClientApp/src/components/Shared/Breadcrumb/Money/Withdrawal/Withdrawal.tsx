import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { withModals } from "store/middlewares/modal/container";
import { withUserAccountWithdrawalBundles } from "store/root/user/account/withdrawal/bundles/container";
import { compose } from "recompose";

const Withdrawal: FunctionComponent<any> = ({ modals, actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => modals.showWithdrawalModal(actions, amounts)}>
    Withdrawal Money
  </Button>
);

const enhance = compose<any, any>(
  withUserAccountWithdrawalBundles,
  withModals
);

export default enhance(Withdrawal);
