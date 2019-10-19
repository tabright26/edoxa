import React from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { MONEY } from "types";
import { withUserAccountDepositBundles } from "store/root/user/account/deposit/bundles/container";

const DepositButton = ({ modals, bundles: { data, loading } }) => (
  <Button color="primary" size="sm" disabled={loading} block onClick={() => modals.showDepositModal(MONEY, data)}>
    Deposit Money
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountDepositBundles
);

export default enhance(DepositButton);
