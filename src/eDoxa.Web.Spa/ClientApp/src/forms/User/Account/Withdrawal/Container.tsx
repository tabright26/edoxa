import { connect, MapStateToProps } from "react-redux";
import { accountWithdrawal } from "store/root/user/account/withdrawal/actions";
import Withdrawal from "./Withdrawal";
import { RootState } from "store/types";
import { Bundle, Currency } from "types";

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
  state,
  ownProps
) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    accountWithdrawal: (data: any) =>
      dispatch(accountWithdrawal(ownProps.currency, data.bundle))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Withdrawal);
