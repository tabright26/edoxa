import React from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { TOKEN } from "types";
import { withUserAccountDepositBundles } from "store/root/user/account/deposit/bundles/container";

const DepositButton = ({ modals, bundles: { data, loading } }) => (
  <Button color="primary" size="sm" block disabled={loading} onClick={() => modals.showDepositModal(TOKEN, data)}>
    Buy Token
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountDepositBundles
);

export default enhance(DepositButton);
