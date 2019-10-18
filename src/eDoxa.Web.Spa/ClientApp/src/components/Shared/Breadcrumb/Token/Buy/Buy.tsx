import React from "react";
import { Button } from "reactstrap";
import { withUserAccountDepositBundles } from "store/root/user/account/deposit/bundles/container";
import { withModals } from "store/middlewares/modal/container";
import { compose } from "recompose";

const DepositButton = ({ modals, actions, amounts }) => (
  <Button color="primary" size="sm" block onClick={() => modals.showDepositModal(actions, amounts)}>
    Buy Token
  </Button>
);

const enhance = compose<any, any>(
  withUserAccountDepositBundles,
  withModals
);

export default enhance(DepositButton);
