import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Balance";
import BuyToken from "./Buy";
import { CURRENCY_TOKEN } from "types";

const TokenBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge
        id="tokenPopover"
        className="bg-gray-900"
        style={{ width: "100px" }}
      >
        <Balance currency={CURRENCY_TOKEN} attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "250px"
        }}
        placement="bottom"
        isOpen={open}
        target={"tokenPopover"}
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader>TOKEN BALANCE</PopoverHeader>
        <PopoverBody>
          <dl className="row mb-0">
            <dt className="col-6">Available</dt>
            <dd className="col-6">
              <Balance
                currency={CURRENCY_TOKEN}
                attribute="available"
                alignment="right"
              />
            </dd>
            <dt className="col-6">Pending</dt>
            <dd className="col-6">
              <Balance
                currency={CURRENCY_TOKEN}
                attribute="pending"
                alignment="right"
              />
            </dd>
          </dl>
          <BuyToken />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default TokenBreadcrumb;
