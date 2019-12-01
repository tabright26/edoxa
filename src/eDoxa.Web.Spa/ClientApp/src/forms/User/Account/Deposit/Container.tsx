import { connect, MapStateToProps } from "react-redux";
import { accountDeposit } from "store/root/user/account/deposit/actions";
import Deposit from "./Deposit";
import { RootState } from "store/types";
import { Currency, Bundle } from "types";

interface OwnProps {
  currency: Currency;
  bundles: Bundle[];
}

interface StateProps {
  initialValues: {
    bundle: number;
  };
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state: RootState,
  ownProps: OwnProps
) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    accountDeposit: (data: any) =>
      dispatch(accountDeposit(ownProps.currency, data.bundle))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Deposit);
