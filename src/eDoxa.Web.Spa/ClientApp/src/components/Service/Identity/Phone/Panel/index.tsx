import React, { useState, FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import Badge from "components/Shared/Badge";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import UserPhoneForm from "components/Service/Identity/Phone/Form";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { RootState } from "store/types";
import { loadUserPhone } from "store/actions/identity";
import Button from "components/Shared/Button";
import { Phone } from "types/identity";

type OwnProps = {};

type StateProps = {
  phone?: Phone;
  loading: boolean;
};

type DispatchProps = {
  loadUserPhone: () => void;
};

type InnerProps = StateProps & DispatchProps;

type OutterProps = {
  className?: string;
};

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  className,
  phone,
  loading,
  loadUserPhone
}) => {
  useEffect((): void => {
    if (phone === null) {
      loadUserPhone();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setButtonDisabled] = useState(false);
  const disabled = !phone || buttonDisabled;
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">PHONE</strong>
        {phone && (
          <Badge.Verified className="ml-3 my-auto" verified={phone.verified} />
        )}
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faEdit}
          size="sm"
          uppercase
          disabled={disabled}
          onClick={() => setButtonDisabled(true)}
        >
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted mb-0">Number</dd>
            <dd className="col-sm-5 mb-0">
              {disabled && (
                <UserPhoneForm.Update
                  handleCancel={() => setButtonDisabled(false)}
                />
              )}
              {!disabled && <span>{phone.number}</span>}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    phone: state.root.user.phone.data,
    loading: state.root.user.phone.loading
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadUserPhone: () => dispatch(loadUserPhone())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Panel);
