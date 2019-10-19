import { connect } from "react-redux";
import { loadUserAddressBook, deleteUserAddress } from "store/root/user/addressBook/actions";
import { UserAddressBookActions, DELETE_USER_ADDRESS_FAIL } from "store/root/user/addressBook/types";
import Delete from "./Delete";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    deleteUserAddress: () =>
      dispatch(deleteUserAddress(ownProps.addressId)).then((action: UserAddressBookActions) => {
        switch (action.type) {
          case DELETE_USER_ADDRESS_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserAddressBook());
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Delete);
