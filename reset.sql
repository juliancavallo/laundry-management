delete Traceability
delete Log
delete RoadmapShippings
delete RoadmapReception
delete Roadmap
delete ShippingDetail
delete Shipping
delete ReceptionDetail
delete Reception
delete VerticalCheckDigit where TableName = 'Shipping'

update Item set IdItemStatus = 1, Washes = 10

update Item set IdLocation = 1 where Id < 11
update Item set IdLocation = 4 where Id > 10